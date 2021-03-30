import React, { useState, useEffect } from 'react';
import axios from '../../../axios';
import { DataGrid } from '@material-ui/data-grid';
import { Button } from '@material-ui/core';


const columns = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'email', headerName: 'Email', width: 130 },
  { field: 'userName', headerName: 'Login', width: 130 },
  { field: 'firstName', headerName: 'FirstName', width: 130 },
  { field: 'lastName', headerName: 'LastName', width: 130 },
  { field: 'patronymic', headerName: 'Patronymic', width: 130 },

];
const Users = (props) => {
  const [users, changeUsers] = useState([]);
  const [teachers, changeTeachers] = useState([]);
  const [admins, changeAdmins] = useState([]);

  const getUsers = () =>
    axios.get('authentication/getusers')
      .then(response => {
        changeUsers(response.data.users);
        changeTeachers(response.data.teachers);
        changeAdmins(response.data.admins);
      })
      .catch(error => {
        alert(error);
      });

  useEffect(() => {
    getUsers();
  }, [])

  const changeRoleHandler = (userId, role, oldRole) => {
      axios.post(`authentication/changerole/${userId}/${role}/${oldRole}`)
      .then(response => {
        getUsers();
      })
      .catch(error => {
        alert(error);
      });
  }
  return (
    <>
      <div style={{ height: 400, width: '100%' }}>
        <h1>Users</h1>
        <DataGrid rows={users} disableMultipleSelection={true} columns={[
          ...columns, {
            field: '', headerName: '', width: 250, renderCell: (e) => (
              <strong>
                <Button
                  variant="contained"
                  color="primary"
                  size="small"
                  style={{ marginLeft: 16 }}
                  onClick={() => changeRoleHandler(e.row.id, 'Teacher', 'User')}
                >
                  To teacher
             </Button>
              </strong>
            )
          },]} pageSize={5} />
      </div>
      <div style={{ height: 400, width: '100%' }}>
        <h1>Teachers</h1>
        <DataGrid rows={teachers} disableMultipleSelection={true} columns={[
          ...columns, {
            field: '', headerName: '', width: 250, renderCell: (e) => (
              <strong>
                <Button
                  variant="contained"
                  color="primary"
                  size="small"
                  onClick={() => changeRoleHandler(e.row.id, 'User', 'Teacher')}
                  style={{ marginLeft: 16 }}
                >
                  To User
             </Button>
                <Button
                  variant="contained"
                  color="primary"
                  size="small"
                  style={{ marginLeft: 16 }}
                  onClick={() => changeRoleHandler(e.row.id, 'Administrator', 'Teacher')}
                >
                  To admin
             </Button>
              </strong>
            )
          },]} pageSize={5} />
      </div>
      <div style={{ height: 400, width: '100%' }}>
        <h1>Admins</h1>
        <DataGrid rows={admins} disableMultipleSelection={true} columns={[
          ...columns, {
            field: '', headerName: '', width: 250, renderCell: (e) => (
              <strong>
                <Button
                  variant="contained"
                  color="primary"
                  size="small"
                  style={{ marginLeft: 16 }}
                  onClick={() => changeRoleHandler(e.row.id, 'Teacher', 'Administrator')}
                >
                  To teacher
             </Button>
              </strong>
            )
          },]} pageSize={5} />
      </div>
    </>
  );
}

export default Users;