const express = require('express');
const multer = require('multer');
const path = require('path');

const uploadDir = '/uploads';       // Mount this in Docker
const app = express();

// Store files in /uploads (make sure it exists in the container)
const storage = multer.diskStorage({
    destination: uploadDir,
    filename: (req, file, cb) => {
        cb(null, file.originalname)  // Save using original filename
    }
});

const upload = multer({ storage });

app.post('/upload', upload.single('file'), (req, res) => {
    console.log('Received file: req.file.originalname');
    res.status(200).send('File stored');
});

app.listen(8080, () => {
    console.log('File server running on port 8080');
});
