<template>
  <div class="input">
    <div
      class="input__label"
      @mousemove="
        (event) => {
          if (tooltip) {
            $msEmitter.emit('showTooltip', event, tooltip);
          }
        }
      "
      @mouseout="$msEmitter.emit('hideTooltip')"
    >
      {{ title }}
    </div>

    <div class="list-item" :class="direction">
      <label v-for="item in items" :key="item.id">
        <input
          type="radio"
          :value="item.id"
          :checked="item.id == id"
          @click="itemOnSelect(item)"
          @keypress.enter="focusNext($event.target)"
          :disabled="readonly"
        />
        {{ item.value }}
      </label>
    </div>
  </div>
</template>

<script>
import mixin from "../../js/components/input/MSInputRadio.js";
export default {
  mixins: [mixin],
};
</script>

<style lang="scss" scoped>
.input {
  .input__label {
    font-weight: bold;
  }
  .column {
    flex-direction: column;
  }
  .row {
    flex-direction: row;
  }
  .list-item {
    display: flex;
    label {
      height: 20px;
      display: flex;
      align-items: center;
    }
  }
}
</style>