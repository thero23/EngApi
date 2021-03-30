import React, { useEffect, useState } from 'react';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { Button, TextField, } from '@material-ui/core';
import axios from '../../../../axios';
import { AddCircleTwoTone, DeleteTwoTone, MoreTwoTone } from '@material-ui/icons';
import { red, blue } from '@material-ui/core/colors';
import IconButton from '@material-ui/core/IconButton';
import ReactQuill, { Quill } from "react-quill";

// #1 import quill-image-uploader
import ImageUploader from "quill-image-uploader";
import './style.css';

// #2 register module
Quill.register("modules/imageUploader", ImageUploader);


const modules = {
  toolbar: [
    [{ 'header': '1' }, { 'header': '2' }, { 'font': [] }],
    [{ size: [] }],
    ['bold', 'italic', 'underline', 'strike', 'blockquote'],
    [{ 'list': 'ordered' }, { 'list': 'bullet' },
    { 'indent': '-1' }, { 'indent': '+1' }],
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

export const SubsectionDetails = ({ subsectionId, setSectionPage, handleClickOpen, isOpen }) => {
  const [subsection, setSubsection] = useState({});
  const [editSubsection, setEditSubsection] = useState({});

  const [exercises, setExercises] = useState([]);
  const [editMode, setEdit] = useState(false);


  const getSubsection = () => {
    axios.get(`/subsections/${subsectionId}`)
      .then(response => {
        const item = response.data;
        setEditSubsection(item);
        setSubsection(item);
      })
      .catch(error => {

      })
  }

  const updateSubsection = () => {
    axios.put(`/subsections`, editSubsection)
      .then(response => {
        setEdit(false);
        getSubsection();
      })
      .catch(error => {

      })
  }

  const getExercises = () => {
    axios.get(`/subsections/${subsectionId}/exercises`)
      .then(response => {
        const item = response.data;
        setExercises(item);
      })
      .catch(error => {
        alert(error);
      })
  }

  useEffect(() => {
    getSubsection();
    getExercises();
  }, [isOpen])

  const PrintElem = () => {
    var mywindow = window.open('', 'PRINT', 'height=800,width=600');

    mywindow.document.write('<html><head><title></title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + subsection.name + '</h1>');
    mywindow.document.write(document.getElementById('lecture-print').innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();

    mywindow.print();
    mywindow.close();

    return true;
  }
  return (
    <Grid container spacing={3}>
      <Grid item xs={9}>
        <Paper>[{subsection.order}]{subsection.name}</Paper>
        {editMode && (<Paper>
          <TextField
            value={editSubsection.order}
            lable='Order'
            type='number'
            onChange={(e) => setEditSubsection({
              ...editSubsection,
              order: +e.target.value > 0 ? +e.target.value : 1,
            })}
            required
          />
          <TextField
            value={editSubsection.name}
            lable='Title'
            onChange={(e) => setEditSubsection({
              ...editSubsection,
              name: e.target.value,
            })}
            required
          />
        </Paper>)}
      </Grid>
      <Grid item xs={12}>
        <Paper id="lecture-print">
          <div align='justify' dangerouslySetInnerHTML={{ __html: subsection.lecture }} />
          {editMode && (<ReactQuill
            theme="snow"
            value={editSubsection.lecture}
            onChange={e => setEditSubsection({
              ...editSubsection,
              lecture: e,
            })}
            modules={modules}
            formats={formats}
          />)}
        </Paper>
      </Grid>
      <Button onClick={() => PrintElem()}>Print lecture</Button>
      <Grid item xs={12}>
        <h2>Exercises</h2>
        {exercises.map((e) => {
        return (
          <Paper key={e.id}>
            {e.title}
            <IconButton aria-label="details" >
              <MoreTwoTone
                fontSize="default"
                style={{ color: blue[500] }}
                onClick={() => setSectionPage({
                  page: 'exercise',
                  item: e,
                  second: subsectionId,
                })} />
            </IconButton>
            {editMode && (
              <IconButton aria-label="delete" >
                <DeleteTwoTone fontSize="default" style={{ color: red[500] }} onClick={() => handleClickOpen('deleteExercise', subsectionId, e.id)} />
              </IconButton>)}
          </Paper>
        )
      })}
        {editMode && (<IconButton aria-label="delete" onClick={() => handleClickOpen('addExercises', subsectionId, getSubsection)} >
          <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
        </IconButton>)}

      </Grid>
      <Grid item xs={3}>
        {!editMode && (<Button
          onClick={() => setEdit(!editMode)} color="primary" autoFocus>
          Edit
        </Button>) || (
            <Button
              onClick={() => updateSubsection()} color="primary" autoFocus>
              Save
            </Button>
          )}
        <Button
          onClick={() => setSectionPage({
            page: 'section',
            item: null,
          })} color="primary" autoFocus>
          Back to section
      </Button>
      </Grid>
    </Grid>
  );
}
