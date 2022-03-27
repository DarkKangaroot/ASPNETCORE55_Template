<template>
    <div>
        <v-dialog
        ref="dialog"
        v-model="modal"
        :return-value.sync="date"
        width="350px"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="date"
            :label="label"
            readonly
            v-bind="attrs"
            v-on="on"
            outlined
            dense
            :rules="rules"
          ></v-text-field>
        </template>
        <v-date-picker
          color="purple darken-2"
          v-model="date"
        >
          <v-spacer></v-spacer>
          <!-- <v-btn
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
          </v-btn> -->
        </v-date-picker>
      </v-dialog>
    </div>
</template>

<script lang="ts">
import { Mixins, Component, Ref, Prop, Watch  } from 'vue-property-decorator'
import { VDialog } from "@/plugins/vuetify"
import Mixin from "@/plugins/mixins.vue"

@Component({})
export default class DatePickerComponent extends Mixins(Mixin) {
    
    @Ref("dialog") readonly dialog!: VDialog;

    @Prop({default: 'Select Date'}) readonly label!: string
    @Prop() readonly rules!:  boolean | string
    @Prop() readonly passValue!: string;

    modal = false;
    date = "";

    @Watch(`passValue`, { deep: true, immediate: true })
    watch_passValue(val: string): void{
      // if(!val) return;

      this.date = val;
    }

    @Watch(`date`, { deep: true, immediate: true })
    watch_dateValue(val: string): void{
      if(!val) return;
      this.okModal();
    }

    okModal(): void {
        this.dialog.save(this.date)
        this.$emit("selectedDate", this.date);
    }
}
</script>