import React, { useState } from 'react';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { Button, TextField, } from '@material-ui/core';
import axios from '../../../../axios';
import 'react-quill/dist/quill.snow.css';
import ReactQuill, { Quill } from "react-quill";

// #1 import quill-image-uploader
import ImageUploader from "quill-image-uploader";

// #2 register module
Quill.register("modules/imageUploader", ImageUploader);


const modules = {
  toolbar: [
    [{ 'header': '1'}, {'header': '2'}, { 'font': [] }],
    [{size: []}],
    ['bold', 'italic', 'underline', 'strike', 'blockquote'],
    [{'list': 'ordered'}, {'list': 'bullet'}, 
     {'indent': '-1'}, {'indent': '+1'}],
    ['link', 'image', 'video'],
    ['clean']
  ],
  imageUploader: {
    upload: file => {
      return new Promise((resolve, reject) => {
        const formData = new FormData();
        formData.append("image", file);

        fetch(
          "https://api.imgbb.com/1/upload?key=d36eb6591370ae7f9089d85875e56b22",
          {
            method: "POST",
            body: formData
          }
        )
          .then(response => response.json())
          .then(result => {
            console.log(result);
            resolve(result.data.url);
          })
          .catch(error => {
            reject("Upload failed");
            console.error("Error:", error);
          });
      });
    }
  },
  clipboard: {
    matchVisual: false,
  }
};

const formats = [
  'header', 'font', 'size',
  'bold', 'italic', 'underline', 'strike', 'blockquote',
  'list', 'bullet', 'indent',
  'link', 'image', 'video'
]


export const AddSubsection = ({ setSectionPage, sectionId }) => {
  const [subsection, setSubsection] = useState({
    name: '',
    lecture: '',
    order: 1,
    sectionId,
  });

  const addSubsection = () => {
    axios.post(`/subsections`, subsection)
      .then(() => {
        setSectionPage({
          page: 'section',
          item: null,
        })
      })
      .catch(error => {
        alert(error);
      })
  }
  return (
    <Grid container spacing={3}>
      <Grid item xs={9}>
        <TextField
          id="outlined-multiline-static"
          label="Order"
          type="number"
          value={subsection.order}
          onChange={(e) => setSubsection({
            ...subsection,
            order: +e.target.value < 1 ? 1 : +e.target.value,
          })}
          variant="outlined"
        />
        <TextField
          id="outlined-multiline-static"
          label="Name"
          multiline
          value={subsection.name}
          onChange={(e) => setSubsection({
            ...subsection,
            name: e.target.value,
          })}
          variant="outlined"
        />

      </Grid>
      <Grid item xs={12}>
        <ReactQuill
          theme="snow"
          value={subsection.lecture}
          onChange={e => setSubsection({
            ...subsection,
            lecture: e,})}
            modules={modules}
            formats={formats}
        />
      </Grid>
      <Grid item xs={12}>
        <Paper >
          <div dangerouslySetInnerHTML={{ __html: subsection.lecture }} />
        </Paper>
      </Grid>
      <Grid item xs={3}>
        <Button
          onClick={() => setSectionPage({
            page: 'section',
            item: null,
          })} color="primary" autoFocus>
          Back
      </Button>
        <Button
          onClick={() => addSubsection()} color="primary" autoFocus>
          Save
      </Button>
      </Grid>
    </Grid>
  );
}
