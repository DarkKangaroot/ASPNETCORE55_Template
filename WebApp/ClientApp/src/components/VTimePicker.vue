<template>
    <div>
        <v-dialog
        ref="dialog"
        v-model="modal"
        :return-value.sync="time"
        width="350px"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="timeFormat"
            :label="label"
            readonly
            v-bind="attrs"
            v-on="on"
            outlined
            dense
            :rules="rules"
          ></v-text-field>
        </template>
        <v-time-picker
          color="purple darken-2"
          v-model="time"
          ampm-in-title
          scrollable
          format="ampm"
        >
          <v-spacer></v-spacer>
          <v-btn
            text
            color="primary"
            @click="modal = false"
          >
            Cancel
          </v-btn>
          <v-btn
            text
            color="primary"
            @click="okModal"
          >
            OK
          </v-btn>
        </v-time-picker>
      </v-dialog>
    </div>
</template>

<script lang="ts">
import { Mixins, Component, Ref, Prop, Watch  } from 'vue-property-decorator'
import { VDialog } from "@/plugins/vuetify"
import Mixin from "@/plugins/mixins.vue"

@Component({})
export default class TimePickerComponent extends Mixins(Mixin) {
    
    @Ref("dialog") readonly dialog!: VDialog;

    @Prop({default: 'Select Time'}) readonly label!: string
    @Prop() readonly rules!:  boolean | string
     @Prop() readonly passValue!: string;

    modal = false;
    time = "";
    timeFormat = "";

    @Watch(`passValue`, { deep: true, immediate: true })
    watch_passValue(val: string): void {
      // if(!val) return;

      this.timeFormat = val;
    }

    @Watch("time")
    watch_time(val: string): void{
      if(!val) return

      this.timeFormat = this.setFormat12hrTime(val);
    }

    okModal(): void {
        this.dialog.save(this.time)
        this.$emit("selectedDate", this.timeFormat);
    }
}
</script>