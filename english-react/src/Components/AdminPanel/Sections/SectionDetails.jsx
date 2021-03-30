import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import { Accordion, AccordionActions, AccordionSummary, Button, Container, TextField, Typography } from '@material-ui/core';
import axios from '../../../axios';
import IconButton from '@material-ui/core/IconButton';
import { AddCircleTwoTone, DeleteTwoTone, MoreTwoTone, ExpandMore } from '@material-ui/icons';
import { red, blue } from '@material-ui/core/colors';
import { Dialog } from '@material-ui/core';
import DeleteUser from './Modals/DeleteUser';
import UserDetails from './Modals/UserDetails';
import DictionaryDetails from './Modals/DictionaryDetails';
import AddUsers from './Modals/AddUser';
import AddDictionary from './Modals/AddDictionary';
import DeleteDictionary from './Modals/DeleteDictionary';
import { UsersPanel } from './partial-components/UsersPanel';
import { SubsectionDetails } from './partial-components/SubsectionDetails';
import { DictionariesPanel } from './partial-components/DictionariesPanel';
import AddExercises from './Modals/AddExercises';
import DeleteExercise from './Modals/DeleteExercise';
import ExerciseDetails from './partial-components/ExerciseDetails';
import { AddSubsection } from './partial-components/AddSubsections';
import DeleteSubsection from './Modals/DeleteSubsection';



const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    width: '100%',
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
    margin: 3,
  },
  heading: {
    fontSize: theme.typography.pxToRem(15),
  },
  secondaryHeading: {
    fontSize: theme.typography.pxToRem(15),
    color: theme.palette.text.secondary,
  },
  icon: {
    verticalAlign: 'bottom',
    height: 20,
    width: 20,
  },
  details: {
    alignItems: 'center',
  },
  column: {
    flexBasis: '33.33%',
  },
  helper: {
    borderLeft: `2px solid ${theme.palette.divider}`,
    padding: theme.spacing(1, 2),
  },
  link: {
    color: theme.palette.primary.main,
    textDecoration: 'none',
    '&:hover': {
      textDecoration: 'underline',
    },
  },
}));


const SectionDetails = ({ section, pageHandler }) => {
  const [open, setOpen] = useState(false);
  const [modal, setModal] = useState({});
  const [sectionPage, setSectionPage] = useState({
    page: 'section',
    item: null,
    second: null,
  })

  const sectionPageHandler = () => {
    switch (sectionPage.page) {
      case 'section': return <SectionsInfo section={section} setSectionPage={setSectionPage} pageHandler={pageHandler} handleClickOpen={handleClickOpen} isOpen={open} />
      case 'subsection': return <SubsectionDetails subsectionId={sectionPage.item} setSectionPage={setSectionPage} handleClickOpen={handleClickOpen} isOpen={open} />
      case 'exercise': return <ExerciseDetails exercise={sectionPage.item} setSectionPage={setSectionPage} subsectionId={sectionPage.second} />
      case 'addSubsection': return <AddSubsection setSectionPage={setSectionPage} sectionId={section.id} />
      default:
        break;
    }
  }
  const handleClickOpen = (name, id, id2) => {
    switch (name) {
      case 'deleteUser': setModal(<DeleteUser handleClose={handleClose} sectionId={section.id} userId={id} />)
        break;
      case 'deleteDictionary': setModal(<DeleteDictionary handleClose={handleClose} sectionId={section.id} dictionaryId={id} />)
        break;
      case 'addUsers': setModal(<AddUsers handleClose={handleClose} sectionId={id} />)
        break;
      case 'addDictionary': setModal(<AddDictionary handleClose={handleClose} sectionId={id} />)
        break;
      case 'userDetails': setModal(<UserDetails handleClose={handleClose} user={id} />)
        break;
      case 'dictionaryDetails': setModal(<DictionaryDetails handleClose={handleClose} dictionary={id} />)
        break;
      case 'addExercises': setModal(<AddExercises handleClose={handleClose} subsectionId={id} />)
        break;
      case 'deleteExercise': setModal(<DeleteExercise handleClose={handleClose} subsectionId={id} exerciseId={id2} />)
        break;
      case 'deleteSubsection': setModal(<DeleteSubsection handleClose={handleClose} subsectionId={id} />)
        break;
      default:
        alert('error');
        break;
    }
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };


  return (
    <Container>
      <Grid container spacing={3}>
        {sectionPageHandler()}
      </Grid>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        {modal}
      </Dialog>
    </Container>

  );
}

export default SectionDetails;


const SectionsInfo = ({ section, setSectionPage, pageHandler, handleClickOpen, isOpen }) => {
  const [editMode, setEditMode] = useState(false);
  const classes = useStyles();
  const [subsections, changeSubsections] = useState([]);
  const [sectionItem, changeSectionItem] = useState(section);

  const getSections = () => {
    axios.get(`/sections/${sectionItem.id}`)
      .then(response => {
        const item = response.data;
        changeSectionItem(item);
      })
      .catch(error => {
        alert(error);
      })
  }

  const getSubsections = () => {
    axios.get(`/sections/${sectionItem.id}/subsections`)
      .then(response => {
        const item = response.data;
        changeSubsections(item);
      })
      .catch(error => {
        alert(error);
      })
  }
  useEffect(() => {
    getSections();
    getSubsections();
  }, [editMode, isOpen])

  const updateSection = () => {
    axios.put('/sections', sectionItem)
      .then(response => {
        getSections();
      })
      .catch(error => {
        alert(error);
      })
  }
  return (
    <Grid container xs={12} spacing={3}>
      <Grid item xs={12}>
        {(!editMode && <Paper className={classes.paper}>{sectionItem.name}</Paper>)
          || (
            <TextField
              value={sectionItem.name}
              onChange={(e) => changeSectionItem({
                ...sectionItem,
                name: e.target.value,
              })}
              required
            />
          )}
      </Grid>
      <Grid item xs={6}>
        <h2>Subsections</h2>
        {subsections.map((subsection) => {
          return (
            <Accordion defaultExpanded>
              <AccordionSummary
                expandIcon={<ExpandMore />}
                aria-controls="panel1c-content"
                id="panel1c-header"
              >
                <Typography className={classes.heading}>
                  [{subsection.order}]{subsection.name}
                </Typography>
              </AccordionSummary>
              <AccordionActions>
                <IconButton
                  aria-label="details"
                  onClick={() => setSectionPage({
                    page: 'subsection',
                    item: subsection.id,
                  })}
                >
                  <MoreTwoTone fontSize="default" style={{ color: blue[500] }} />
                </IconButton>
                {editMode && (<IconButton aria-label="delete"
                  onClick={() => handleClickOpen('deleteSubsection', subsection.id)}
                >
                  <DeleteTwoTone fontSize="default" style={{ color: red[500] }} />
                </IconButton>)}
              </AccordionActions>
            </Accordion>
          );
        })}
        {editMode && (<IconButton aria-label="delete" disabled={!editMode} onClick={() => setSectionPage({
          page: 'addSubsection',
          item: null,
        })}>
          <AddCircleTwoTone fontSize="default" style={{ color: blue[500] }} />
        </IconButton>)}
      </Grid>
      <UsersPanel editMode={editMode} sectionId={sectionItem.id} handleClickOpen={handleClickOpen} isOpen={isOpen} />
      <DictionariesPanel editMode={editMode} sectionId={sectionItem.id} handleClickOpen={handleClickOpen} isOpen={isOpen} />
      {(!editMode && <Button
        onClick={() => setEditMode(true)}
        color="primary" autoFocus>
        Edit
      </Button>) ||
        (<>
          <Button
            onClick={() => {
              updateSection();
              setEditMode(false);
            }}
            color="primary" autoFocus>
            Save changes
      </Button>
          <Button
            onClick={() => setEditMode(false)}
            color="primary" autoFocus>
            Cancel
      </Button>
        </>)}
      <Button
        onClick={() => pageHandler({
          name: 'list',
          section: null,
        })} color="primary" autoFocus>
        Back to sections
      </Button>
    </Grid>
  )
}


