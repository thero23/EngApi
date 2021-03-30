import React, { useState, useEffect } from 'react';
import axios from '../../../axios';
import Section from './Section/Section';
import './Sections.css';
import subsectionState from '../../../recoilStates/subsectionState';
import { useRecoilState, useRecoilValue } from 'recoil';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import Words from '../Words/Words';
import ExerciseStepper from './Subsection/Exercises/ExerciseStepper';
import './style.css';
import { useHistory } from 'react-router';


function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`wrapped-tabpanel-${index}`}
      aria-labelledby={`wrapped-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box p={3}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

function a11yProps(index) {
  return {
    id: `wrapped-tab-${index}`,
    'aria-controls': `wrapped-tabpanel-${index}`,
  };
}

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    backgroundColor: theme.palette.background.paper,
  },
}));


const Sections = (props) => {

  const [sections, changeSections] = useState([]);
  const [subsections, changeSubsections] = useState([]);
  const [selectedSectionId, changeSelectedSection] = useState(null);
  const [selectedSubsection, setSelectedSubsection] = useRecoilState(subsectionState);
  const [dictionaries, setDictionaries] = useState([]);
  const [selectedDict, setSelectedDict] = useState();
  const [exercises, setExercises] = useState([]);
  const history = useHistory();

  const classes = useStyles();
  const [value, setValue] = useState('one');

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  useEffect(() => {
    setSelectedSubsection(null);
    setSelectedDict(null);
    setExercises([]);
  }, [selectedSectionId])


  useEffect(() => {
    if(selectedSubsection) getExercises();
  }, [selectedSubsection])
  
  useEffect(() => {
    setSelectedSubsection({});
    axios.get("/sections")
      .then(response => {
        const sect = response.data;
        changeSections(sect);
      })
      .catch(error => {
        history.push('/authentication');
      });
  }, []);

  const getDictionaries = (id) => {
    axios.get(`sections/${id}/dictionaries`)
      .then(response => {
        const dict = response.data;
        setDictionaries(dict);
      })
      .catch(error => {
        history.push('/authentication');
      });
  };

  const getExercises = () => {
    axios.get(`/subsections/${selectedSubsection.id}/exercises`)
      .then(response => {
        const item = response.data;
        setExercises(item);
      })
      .catch(error => {
        history.push('/authentication');
      })
  }


  const sectionToggleHandler = (id) => {
    selectedSectionId === id ? changeSelectedSection(null) : changeSelectedSection(id);
  }

  useEffect(() => {
    if (selectedSectionId && selectedSubsection !== {}) {
      axios.get(`/sections/${selectedSectionId}/subsections`)
        .then(response => {
          const subsection = response.data;
          changeSubsections(subsection);

        })
        .catch(error => {
          history.push('/authentication');
        });
    }
  }, [selectedSectionId]);

  let sect = sections.map(sec => {
    return (
      <Section key={sec.id} id={sec.id} clicked={() => {
        sectionToggleHandler(sec.id);
        getDictionaries(sec.id);
      }} showId={selectedSectionId} subsections={subsections} name={sec.name} />
    )
  });
  if(sections.length === 0) return <div className="Sections">Sorry, you do not have access to any sections right now. Please notify your teacher. </div>
  return (
    <div className="Sections">
      <div className="Tree">
        {sect}
      </div>
      <div className="Content">
        {selectedSectionId && selectedSubsection && (<div className={classes.root}>
          <AppBar position="static">
            <Tabs value={value} onChange={handleChange}>
              <Tab value="one" label="Lecture" wrapped {...a11yProps('one')} />
              <Tab value="two" label="Tasks" {...a11yProps('two')} />
              <Tab value="three" label="Dictionaries" {...a11yProps('three')} />

            </Tabs>
          </AppBar>
          <TabPanel className="content-panel" value={value} index="one">
            <h1>{selectedSubsection.name}</h1>
            <div dangerouslySetInnerHTML={{ __html: selectedSubsection.lecture }} />
          </TabPanel>
          <TabPanel value={value} index="two">
            <ExerciseStepper exercises={exercises}/>
            </TabPanel>
          <TabPanel value={value} index="three">
            <div className="Dictionaries">
            {!selectedDict ? (dictionaries.map(d => (
              <div className="dictionary-item" key={d.id} onClick={() => setSelectedDict(d)}>
                {d.name}
              </div>))) : <Words dictionaryId={selectedDict.id} setSelectedDict={setSelectedDict} />
            }
            </div>
           
          </TabPanel>

        </div>)}

      </div>

    </div>


  );
};

export default Sections;



