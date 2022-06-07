export const apiStore = {
  userLogin: '/user/login',
  userRegister: '/user/register',

  roomGet: '/room/{0}',
  roomGetUsers: '/room/{0}/users',
  roomUser: '/room/user',
  roomCreate: '/room/create',
  roomList: '/room/list',
  roomEnd: '/room/{0}/end',
  roomLeave: '/room/{0}/leave',
  roomKick: '/room/{0}/kick/{1}',
  roomStart: '/room/{0}/start',
  roomPause: '/room/{0}/pause',
  roomEnter: '/room/{0}/enter',
  roomUpdate: '/room/{0}/update',
  roomResume: '/room/{0}/resume',
  roomRemove: '/room/{0}/remove',

  additionList: '/additions/list',
  additionGet: '/additions/{0}',

  hubApi: '/hub',
};