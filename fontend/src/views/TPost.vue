<template>
  <div class="post">
    <div class="view-count flex">
      <div class="icon-container">
        <div class="icon-eye-black"></div>
      </div>
      {{ ViewCount }}
    </div>
    <div class="img__container" @click="postDetailOnClick()">
      <div class="img">
        <img height="165" :src="ImgLink" :alt="aPost.Title" />
      </div>
    </div>
    <div class="info">
      <div
        class="title"
        @click="postDetailOnClick()"
        @mousemove="
          (event) => {
            if (aPost.Title.length > 42) {
              $msEmitter.emit('showTooltip', event, aPost.Title);
            }
          }
        "
        @mouseout="$msEmitter.emit('hideTooltip')"
      >
        {{
          aPost.Title.length > 42
            ? aPost.Title.substring(0, 40) + "..."
            : aPost.Title
        }}
      </div>

      <div class="position">
        <div class="address">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="0.875em"
            viewBox="0 0 384 512"
          >
            <path
              d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"
            />
          </svg>
          {{ aPost.CityName }}
        </div>

        <div class="news-type">{{ NewsType }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TPost",
  components: {},
  computed: {
    NewsType() {
      if (this.news.NewsType == 0) {
        return "Tin cần tìm";
      } else {
        return "Tin nhặt được";
      }
    },
    ViewCount() {
      if (this.news.ViewCount / 1000 > 1) {
        let hundred = Math.floor(this.news.ViewCount / 100);

        return hundred / 10 + "k";
      }
      return this.news.ViewCount;
    },
    ImgLink() {
      return this.news?.ImgLink
        ? this.$msApi.NewsApi.GetImg(this.news.ImgLink)
        : "";
    },
  },
  props: ["news"],

  data() {
    return {
      aPost: this.news ? this.news : {},
    };
  },
  watch: {},

  methods: {
    postDetailOnClick() {
      this.$router.push("/TPostDetail/" + this.news.NewsId);
    },
  },

  created() {},

  mounted() {},

  beforeUnmount() {},
};
</script>

<style lang="scss" scoped>
.post {
  height: 260px;
  width: 280px;
  border-radius: 8px;
  border-left: 1px solid rgba(190, 190, 190, 0.2);
  box-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
  position: relative;

  .view-count {
    align-items: center;
    position: absolute;
    top: 141px;
    left: 0;
    background: #ffc107;
    border-radius: 0 8px 0 0;
    font-size: 0.875rem;
    font-weight: 600;
    padding: 0px 6px;
    z-index: 2;
  }

  .img__container {
    border-top-left-radius: 8px;
    border-top-right-radius: 8px;
    overflow: hidden;
    position: relative;

    .img {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 280px;
      height: 165px;
      cursor: pointer;

      object-fit: cover;
      -o-object-position: center;
      object-position: center;

      transition: 0.2s linear;

      &:hover {
        transform: scale(1.2);
      }
    }
  }

  .info {
    .title {
      padding: 12px;
      font-size: 18px;
      height: 68px;
      font-weight: bold;
      cursor: pointer;
      &:hover {
        color: #28a745;
      }
    }

    .position {
      color: #333;
      display: flex;
      padding: 0px 12px;
      justify-content: space-between;
    }
  }
}
</style>