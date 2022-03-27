
export interface INotification {
    isNotificationShow: boolean,
    color: string,
    message: string
}

export interface IConfirmDialog {
    dialog: boolean,
    message: string,
    title: string,
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    resolve: any,
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    reject: any,
    dialogOptions: {
        color: string,
        width: number,
        noconfirm: boolean,
    },
}