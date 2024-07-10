import os
import joblib
import glob
import random

from pydub import AudioSegment
from pydub.silence import split_on_silence
import librosa
import numpy as np

loaded_model = joblib.load("models/fssp_captcha_svm.pkl")
temp_dir = "./temp"

def get_audio_chunks(filename):
    # Load your audio file (replace with your file path)
    audio_file = AudioSegment.from_file(filename, format='wav')

    # Split audio on silence
    chunks = split_on_silence(
        audio_file, 
        # Specify parameters for silence detection
        min_silence_len=120,     # Minimum silence length in milliseconds
        silence_thresh=-40       # Silence threshold in dB
    )
    return chunks
     
def extract_features(file_path):
    try:
        audio, sr = librosa.load(file_path)
        yt, index = librosa.effects.trim(audio, top_db=10)

        mfcc = librosa.feature.mfcc(y=yt, sr=sr, n_mfcc=64)
        mfcc_mean = np.mean(mfcc, axis=1)
        mfcc_std = np.std(mfcc, axis=1)
        
        return np.concatenate([mfcc_mean, mfcc_std])
    except Exception as e:
        print(f"Error extracting features from file {file_path}: {e}")
        return np.zeros(128)

def predict_chunk(chunk_file):
    global loaded_model
    X = extract_features(chunk_file)
    X = np.array(X).reshape(1, -1)
        
    pred = loaded_model.predict(X)
    return pred[0]
    
def predict_fssp_audio(filename):
    global temp_dir
    chunks = get_audio_chunks(filename)

    if not os.path.exists(temp_dir):
        os.makedirs(temp_dir)
        
    message = ""
    random_part = random.randrange(10000, 99999)

    for i, chunk in enumerate(chunks):
        # Уникальное имя для файла, чтобы другая капча не перезаписала файл 
        filename_chunk = f'{temp_dir}/{random_part}_{i}.wav'
        chunk.export(filename_chunk, format='wav')
        #display(Audio(f'{directory}/{i}.wav', autoplay=False))
        message += predict_chunk(filename_chunk)
        print(message)
        
        # Удаляем файл после обработки
        os.remove(filename_chunk)

    return message
