const mixin = {
  name: "TheNav",
  props: {},
  watch: {},
  data() {
    return {
      navigators: [
        {
          path: "/Overview",
          text: this.$t("Nav.Overview"),
        },
      ],
    };
  },
  computed: {},
  methods: {},
  created() {},
  beforeUnmount() {},
};

export default mixin;
