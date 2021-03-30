import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import Dictionaries from './Dictionaries/Dictionaries';
import Words from './Words/Words';
import Exercises from './Exercises/Exercises';
import Sections from './Sections/Sections';
import axios from '../../axios';
import Users from './Users/Users';
import './AdminPanel.css';

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`scrollable-force-tabpanel-${index}`}
      aria-labelledby={`scrollable-force-tab-${index}`}
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

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.any.isRequired,
  value: PropTypes.any.isRequired,
};

function a11yProps(index) {
  return {
    id: `scrollable-force-tab-${index}`,
    'aria-controls': `scrollable-force-tabpanel-${index}`,
  };
}

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    width: '100%',
    backgroundColor: theme.palette.background.paper,
  },
}));

const AdminPanel = () => {
  const classes = useStyles();
  const [value, setValue] = useState(0);
  const [isAdmin, setIsAdmin] = useState(0);
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  useEffect(() => {
      axios.get('authentication/checkadmin')
        .then((response) => {
          setIsAdmin(response.data.admin);
        }).catch(error => {
          alert(error)
        })
  }, [])
  return (
    <div className='admin-panel'>
      <AppBar position="static" color="default">
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          scrollButtons="on"
          indicatorColor="primary"
          textColor="primary"
          aria-label="scrollable force tabs example"
        >
          <Tab label="Words" {...a11yProps(0)} />
          <Tab label="Dictionaries"  {...a11yProps(1)} />
          <Tab label="Exercises" {...a11yProps(2)} />
          <Tab label="Sections"  {...a11yProps(3)} />
          {(isAdmin && <Tab label="Users"  {...a11yProps(4)} />)}
        </Tabs>
      </AppBar>
      <TabPanel value={value} index={0}>
        <Words />
      </TabPanel>
      <TabPanel value={value} index={1}>
        <Dictionaries />
      </TabPanel>
      <TabPanel value={value} index={2}>
        <Exercises />
      </TabPanel>
      <TabPanel value={value} index={3}>
        <Sections />
      </TabPanel>
      {(isAdmin) && <TabPanel value={value} index={4}>
        <Users />
      </TabPanel>}
    </div>
  );
}

export default AdminPanel;