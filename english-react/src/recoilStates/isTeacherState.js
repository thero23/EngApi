import {
  atom,
} from 'recoil';

const isTeacherState = atom({
  key: 'isTeacherState',
  default: false,
});

export default isTeacherState;
