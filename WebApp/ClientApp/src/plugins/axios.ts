
import Vue from 'vue';
import axios from 'axios';
import jwtToken from "@/services/jwt.token"
// import store from '@/store/index'
// import notification from '@/store/modules/Notification'
// import router from "@/router/index"
// import { IConfirmDialog } from '@/interfaces/IConfirmDialog';
import store from "@/store/index" 
import { INotification} from "@/interface/INotification"
import NProgress from "nprogress";


const host = window.location.origin;
const subPath = process.env.VUE_APP_PATH;

const config = {
    
    headers: {
      "Authorization" : `bearer ${localStorage.getItem("token")}`
    },
    baseURL: process.env.NODE_ENV === 'development' ? "https://localhost:5001/api" : `${host}${subPath}/api`
    // timeout: 60 * 1000, // Timeout
    // withCredentials: true, // Check cross-site Access-Control
};
const _axios = axios.create(config);
_axios.interceptors.request.use(
    // Do something before request is sent
    (conf) => {
      NProgress.start();
      return conf
    },
    // Do something with request error
    (error) => {
      NProgress.done();
      Promise.reject(error)
    },
);

_axios.interceptors.response.use(
  // Do something with response data
  async (response) => {
      
      const returnType = {
        isSuccess: response.data.isSuccess,
        message: response.data.message,
      };
      const notificationConfig: INotification = {
        isNotificationShow: true,
        color: "success",
        message: returnType.message
      }
      if(returnType.isSuccess !== undefined){
        if(!returnType.isSuccess){
          notificationConfig.color = "error"
          store.dispatch("NotificationStore/action_showNotification", notificationConfig)
        }
        store.dispatch("NotificationStore/action_showNotification", notificationConfig)
      }
      NProgress.done();
      return response;
  },
  // Do something with response error
  async (error) => {
    const { data } = error.response;
    const notificationConfig: INotification = {
      isNotificationShow: true,
      color: "error",
      message: data
    }
    store.dispatch("NotificationStore/action_showNotification", notificationConfig)
    NProgress.done();
    return Promise.reject(error)
  }
  
);

function AxiosPlugin() {
  Vue.prototype.$axios = _axios;
}


Vue.use(AxiosPlugin);
export default _axios;