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
    const [content, changeContent] = useState('');

    const classes = useStyles();
    const [value, setValue] = React.useState('one');

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    useEffect(() => {
        setSelectedSubsection({});
        axios.get("/sections")
            .then(response => {
                const sect = response.data;
                changeSections(sect);
            })
            .catch(error => {
                console.log(error)
            });
    }, []);

    useEffect(() => {
        if (selectedSubsection !== {}) {
            axios.get(`/subsections/${selectedSubsection.id}`)
                .then(response => {
                    const lecture = response.data[0].lecture;
                    changeContent(lecture);
                })
                .catch(error => {
                    console.log(error)
                });
        }
    }, [selectedSubsection]);

    const sectionToggleHandler = (id) => {
        selectedSectionId === id ? changeSelectedSection(null) : changeSelectedSection(id);
    }

    useEffect(() => {
        if (selectedSectionId && selectedSubsection !=={}) {
            axios.get(`/sections/${selectedSectionId}/subsections`)
                .then(response => {
                    const subsection = response.data;
                    changeSubsections(subsection);

                })
                .catch(error => {
                    console.log(error)
                });
        }
    }, [selectedSectionId]);

    let sect = sections.map(sec => {
        return (
            <Section key={sec.id} id={sec.id} clicked={() => sectionToggleHandler(sec.id)} showId={selectedSectionId} subsections={subsections} name={sec.name} />
        )
    });
    return (
      <div className="Sections">
          <div className="Tree">
            {sect}
          </div>
            <div className="Content">
                {selectedSectionId && selectedSubsection &&(<div className={classes.root}>
                    <AppBar position="static">
                        <Tabs value={value} onChange={handleChange} aria-label="wrapped label tabs example">
                            <Tab
                                value="one"
                                label="Lecture"
                                wrapped
                                {...a11yProps('one')}
                            />
                            <Tab value="two" label="Tasks" {...a11yProps('two')} />
                            <Tab value="three" label="Test" {...a11yProps('three')} />
                            <Tab value="four" label="Multimedia" {...a11yProps('four')} />
                            <Tab value="five" label="Dictionaries" {...a11yProps('five')} />
                            
                        </Tabs>
                    </AppBar>
                    <TabPanel value={value} index="one">
                        <h1>{selectedSubsection.name}</h1>
                        {content}
                    </TabPanel>
                    <TabPanel value={value} index="two">
                        Item Two
                    </TabPanel>
                    <TabPanel value={value} index="three">
                        Item Three
                    </TabPanel>
                    
                </div>)}

            </div>

        </div>


    );
};

export default Sections;



