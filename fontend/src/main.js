import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { vue3Debounce } from "vue-debounce";
import App from "./App.vue";
import MSLoading from "./components/popup/MSLoading.vue";
import MSInputDate from "./components/input/MSInputDate.vue";
import MSInputCombobox from "./components/input/MSInputCombobox.vue";
import MSInputComboboxMultiSelect from "./components/input/MSInputComboboxMultiSelect.vue";
import MSInputRadio from "./components/input/MSInputRadio.vue";
import MSCheckBox from "./components/input/MSCheckBox.vue";
import MSInputText from "./components/input/MSInputText.vue";
import MSInputSearch from "./components/input/MSInputSearch.vue";
import MSInputVerified from "./components/input/MSInputVerified.vue";
import MSInputVerifying from "./components/input/MSInputVerifying.vue";
import MSDialog from "./components/popup/MSDialog.vue";
import MSReminderNotice from "./components/popup/MSReminderNotice.vue";
import MSNoticeMessage from "./components/popup/MSNoticeMessage.vue";
import MSTooltip from "./components/popup/MSTooltip.vue";
import MSCombobox from "./components/combobox/MSCombobox.vue";
import MenuFeature from "./views/MenuFeature.vue";

import router from "./router/index.js";
import emitter from "tiny-emitter/instance";
import msAxios from "./js/axios/msAxios";
import MSEnum from "./js/enum/enum";
import CustomerEnum from "./js/enum/customerEnum";

import MSApi from "./js/constant/api";

import MSResource from "./js/resource/resource";

import DateFormater from "./js/formater/datetime";

import VueNumerals from "vue-numerals";
import Cookies from "js-cookie";

const i18n = createI18n({
  locale: "VN",
  messages: MSResource,
});

// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
// import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyDq-1BUBeYrTExJCBycIBORCC3684THeTA",
  authDomain: "timdothatlac-83578.firebaseapp.com",
  projectId: "timdothatlac-83578",
  storageBucket: "timdothatlac-83578.appspot.com",
  messagingSenderId: "410770968373",
  appId: "1:410770968373:web:a27b700ce17acc04c3067c",
  measurementId: "G-C41BLSCVSL",
};

initializeApp(firebaseConfig);

const app = createApp(App);
app.use(i18n);
app.component("MenuFeature", MenuFeature);
app.component("MSLoading", MSLoading);
app.component("MSCombobox", MSCombobox);
app.component("MSDialog", MSDialog);
app.component("MSInputCombobox", MSInputCombobox);
app.component("MSInputComboboxMultiSelect", MSInputComboboxMultiSelect);
app.component("MSInputDate", MSInputDate);
app.component("MSInputRadio", MSInputRadio);
app.component("MSCheckBox", MSCheckBox);
app.component("MSInputText", MSInputText);
app.component("MSInputSearch", MSInputSearch);
app.component("MSInputVerified", MSInputVerified);
app.component("MSInputVerifying", MSInputVerifying);
app.component("MSTooltip", MSTooltip);
app.component("MSReminderNotice", MSReminderNotice);
app.component("MSNoticeMessage", MSNoticeMessage);

app.use(router);

// Sử dụng thư viện format số (1.000 bản ghi)
app.use(VueNumerals, {
  locale: "vi",
});

app.config.globalProperties.$msEmitter = emitter;
app.config.globalProperties.$msAxios = msAxios;
app.config.globalProperties.$msEnum = MSEnum;
app.config.globalProperties.$customerEnum = CustomerEnum;
app.config.globalProperties.$msApi = MSApi;
app.config.globalProperties.$msResource = MSResource;
app.config.globalProperties.$msGlobalDepartments = [];
app.config.globalProperties.$msGlobalPositions = [];
app.config.globalProperties.$msDateFormater = DateFormater;
app.config.globalProperties.$msCookies = Cookies;

const clickOutside = {
  beforeMount: (el, binding) => {
    el.clickOutsideEvent = (event) => {
      // here I check that click was outside the el and his children
      if (!(el == event.target || el.contains(event.target))) {
        // and if it did, call method provided in attribute value
        binding.value();
      }
    };
    document.addEventListener("click", el.clickOutsideEvent);
  },
  unmounted: (el) => {
    document.removeEventListener("click", el.clickOutsideEvent);
  },
};

app.directive("click-outside", clickOutside);

app.directive("debounce", vue3Debounce({ lock: true }));
app.mount("#app");
