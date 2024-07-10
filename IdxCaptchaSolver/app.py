"""
This script runs the application using a development server.
It contains the definition of routes and views for the application.
"""

import os
import joblib
import pathlib

from gibdd_captcha import *
from fssp_audio_captcha import predict_fssp_audio
import numpy as np
from flask import Flask, request, jsonify
app = Flask(__name__)

# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app

loaded_model = joblib.load("models/fssp_captcha_svm.pkl")
data_dir = "./datas/"

if not os.path.exists(data_dir):
    os.makedirs(data_dir)

@app.route('/fssp-audio', methods=['POST'])
def upload_audio():
    global data_dir 
    
    if 'file' not in request.files:
        return "No file part", 400
    file = request.files['file']
    if file.filename == '':
        return "No selected file", 400
    if file:
        file_path = os.path.join(data_dir, file.filename)
        file.save(file_path)
        prediction = predict_fssp_audio(file_path)
        os.remove(file_path)
        
        return jsonify(
            status = 0,
            data = prediction
        )

@app.route('/gibdd-captcha', methods=['POST'])
def gibdd_captcha():

    print(request.files)

    if 'file' not in request.files:
        return jsonify({'error': 'No file part in the request'}), 400
    
    file = request.files['file']
    file_ext = pathlib.Path(file.filename).suffix
    
    if  file.filename == '':
        return jsonify({'error': 'No selected file'}), 400
    
    if file_ext == '.jpeg' or file_ext == '.jpg':
        temp_path = os.path.join('temp', file.filename)
        file.save(temp_path)

        img_width = 150
        img_height = 80
        image = preprocess_image(temp_path, img_width, img_height)
        prediction = prediction_model.predict(image)
        decoded_text = decode_batch_predictions(prediction)[0]

        os.remove(temp_path)

        return jsonify({'prediction': decoded_text})
    
    return jsonify({'error': 'Invalid file format'}), 400

if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
