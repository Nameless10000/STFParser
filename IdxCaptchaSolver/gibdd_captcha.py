import os
import numpy as np

from pathlib import Path
import tensorflow as tf
from tensorflow import keras
from keras import layers
import cv2
import json

class CTCLayer(layers.Layer):
    def __init__(self, name=None):
        super().__init__(name=name)
        self.loss_fn = keras.backend.ctc_batch_cost

    def call(self, y_true, y_pred):
        batch_len = tf.cast(tf.shape(y_true)[0], dtype="int64")
        input_length = tf.cast(tf.shape(y_pred)[1], dtype="int64")
        label_length = tf.cast(tf.shape(y_true)[1], dtype="int64")

        input_length = input_length * tf.ones(shape=(batch_len, 1), dtype="int64")
        label_length = label_length * tf.ones(shape=(batch_len, 1), dtype="int64")

        loss = self.loss_fn(y_true, y_pred, input_length, label_length)
        self.add_loss(loss)

        return y_pred

model = keras.models.load_model('./models/gibdd_captcha.h5', custom_objects={'CTCLayer': CTCLayer})

prediction_model = keras.models.Model(
    model.get_layer(name="image").input, model.get_layer(name="dense2").output
)
prediction_model.summary()

def preprocess_image(image_path, img_width, img_height):
    img = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE)
    img = cv2.resize(img, (img_width, img_height))
    img = img / 255.0
    img = np.expand_dims(img, axis=-1)
    img = np.expand_dims(img, axis=0)
    img = np.transpose(img, (0, 2, 1, 3))
    return img

img_width = 150
img_height = 80
max_length = 5

def decode_batch_predictions(pred):
    input_len = np.ones(pred.shape[0]) * pred.shape[1]
    results = keras.backend.ctc_decode(pred, input_length=input_len, greedy=True)[0][0][
              :, :max_length
              ]
    output_text = []
    for res in results:
        res = tf.strings.reduce_join(num_to_char(res)).numpy().decode("utf-8")
        output_text.append(res)
    return output_text

with open('./char/char_to_num.json', 'r') as f:
    char_to_num_config = json.load(f)

with open('./char/num_to_char.json', 'r') as f:
    num_to_char_config = json.load(f)

char_to_num = layers.StringLookup.from_config(char_to_num_config)
num_to_char = layers.StringLookup.from_config(num_to_char_config)