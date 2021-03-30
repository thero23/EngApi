import React, { useState } from 'react';
import SectionList from './SectionsList';
import { Dialog } from '@material-ui/core';
import DeleteSection from './DeleteSection';
import SectionDetails from './SectionDetails';
import AddSection from './partial-components/AddSection';

const Sections = (props) => {
  const [open, setOpen] = useState(false);
  const [modal, setModal] = useState({});
  const [page, setPage] = useState({
    name: 'list',
    section: null,
  });
  const handleClickOpen = (name, id, getItems) => {
    switch (name) {
      case 'delete': setModal(<DeleteSection handleClose={handleClose} sectionId={id} getItems={getItems}/>)
        break;
      default:
        break;
    }
    setOpen(true);
  }; 

  const handleClose = () => {
    setOpen(false);
  };



  const actionPage = () => {
    switch (page.name) {
      case 'details': return <SectionDetails  handleClickOpen={handleClickOpen}  pageHandler={setPage} section={page.section} />
      case 'list': return <SectionList  handleClickOpen={handleClickOpen}  pageHandler={setPage} />
      case 'add': return <AddSection  handleClickOpen={handleClickOpen}  pageHandler={setPage} />
      default:
        return null;
    }
  }

    return (
      <>
        <div style={{ minHeight: '68vh'}}>
          {actionPage()}
        </div>
        <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
          {modal}
        </Dialog>
      </>
    );
  };
  export default Sections;
