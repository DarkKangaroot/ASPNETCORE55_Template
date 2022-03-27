

import {
    VuexModule
    , Module
    , Mutation
    , Action
}
from 'vuex-module-decorators'

import { INotification, IConfirmDialog } from "@/interface/INotification"


@Module({ namespaced: true })
class NotificationStore extends VuexModule {
    //State
    config : INotification = {
        isNotificationShow: false,
        color: "success",
        message: ""
    }

    confirmDialogConfig: IConfirmDialog = {
        dialog: false,
        message: "",
        title: "",
        resolve: null,
        reject: null,
        dialogOptions: {
            color: "purple darken-2",
            width: 400,
            noconfirm: false,
        },
    }
    confirmDialog: Promise<unknown> = new Promise((resolve, reject) => { resolve(false); reject(false) })

    @Mutation
    mutate_showNotification(config: INotification): void {
        this.config = config;
        setTimeout(() => {
            this.config.isNotificationShow = false;
        },2000);
    }

    @Mutation
    mutate_confirmDialog(config: IConfirmDialog): void {
        this.confirmDialogConfig = config;
        if(config.dialogOptions != undefined || config.dialogOptions != null)
        {
            this.confirmDialogConfig.dialogOptions = { ...this.confirmDialogConfig.dialogOptions, ...config.dialogOptions }
        }
        this.confirmDialog = new Promise((resolve, reject) => {
            this.confirmDialogConfig.resolve = resolve;
            this.confirmDialogConfig.reject = reject;
        });
    }

    @Mutation
    mutate_confirmDialogAction(config: IConfirmDialog): void  {
        this.confirmDialogConfig.dialog = config.dialog;
        this.confirmDialogConfig.resolve(config.resolve);
        this.confirmDialogConfig.dialogOptions = {
            color: "primary",
            width: 400,
            noconfirm: false,
        }
    }

    //Action
    @Action({rawError: true})
    action_showNotification(config: INotification): void {
        this.context.commit("mutate_showNotification", config);
    }

    @Action({rawError: true})
    action_confirmDialog(config: IConfirmDialog): void {
        this.context.commit("mutate_confirmDialog", config);
    }

    @Action({rawError: true})
    action_confirmDialogAction(config: IConfirmDialog): void {
        this.context.commit("mutate_confirmDialogAction", config);
    }
}

export default NotificationStore