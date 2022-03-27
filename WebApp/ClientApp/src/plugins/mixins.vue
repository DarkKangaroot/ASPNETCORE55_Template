
<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
import moment from 'moment'
import { IConfirmDialog } from "@/interface/INotification"
import { namespace } from 'vuex-class';
const notificationStore = namespace('NotificationStore');

@Component
export default class Mixin extends Vue {

    rowPerPageOption = [5, 10, 20, 40, 50, 100, 150, 200];

    @notificationStore.State
    confirmDialogConfig!: IConfirmDialog

    @notificationStore.State
    confirmDialog!: Promise<unknown>


    setFormatDate(stringDate: string | Date): string {
        var date = moment(stringDate).format("yyyy-MM-DD");
        if(date == "Invalid date"){
            return "";
        }
        return date;
    }

    setFormat24hrTime(time: string | Date): string {
        return moment(time ,["h:mm A"]).format("HH:mm")
    }

    setFormat12hrTime(time: string | Date): string {
        var date = new Date(`1970-01-01T${time}`)
        if(date.getTime() == date.getTime()){
            return moment(date).format("LT")
        }
        return "";
    }

    toBase64Image(file: File): Promise<string>{
        return new Promise<string>((resolve) => {
            var reader = new FileReader();
            reader.readAsDataURL(file)
            reader.onload = () => { 
                if(reader.result){
                    const result = reader.result as string
                    const base64 = result.split(",");
                    resolve(base64[1]);
                }
            };
        })
    }

    @notificationStore.Action
    action_confirmDialog!: (config: IConfirmDialog) => void

    @notificationStore.Action
    action_confirmDialogAction!: (config: IConfirmDialog) => void

    async initConfirmDialog(title: string, message: string, noconfirm?: boolean): Promise<unknown> {
        const config: IConfirmDialog = {
            dialog: true,
            message: message,
            title: title,
            resolve: null,
            reject: null,
            dialogOptions: {
                color: "purple darken-2",
                width: 400,
                noconfirm: noconfirm ?? false,
            },
        }

        this.action_confirmDialog(config);

        return this.confirmDialog
    }

    agreeConfirmDialog(): void {
        const config: IConfirmDialog = {
            dialog: false,
            message: "",
            title: "",
            resolve: true,
            reject: null,
            dialogOptions: {
                color: "purple darken-2",
                width: 400,
                noconfirm: false,
            },
        }
       this.action_confirmDialogAction(config);
    }

    cancelConfirmDialog(): void {
        const config: IConfirmDialog = {
            dialog: false,
            message: "",
            title: "",
            resolve: false,
            reject: null,
            dialogOptions: {
                color: "purple darken-2",
                width: 400,
                noconfirm: false,
            },
        }
       this.action_confirmDialogAction(config);
    }
} 
</script>