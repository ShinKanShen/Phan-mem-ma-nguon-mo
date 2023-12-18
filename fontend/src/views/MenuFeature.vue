<template>
  <div
    v-show="isOpen"
    :style="{ left: left + 'px', top: top + 'px' }"
    class="menu__list"
    ref="tiepMenu"
  >
    <div v-show="news?.NewsStatus == 0" @click="acceptNews()">Duyệt</div>
    <div v-show="news?.NewsStatus == 1" @click="hideNews()">Ẩn</div>
    <div v-show="news?.NewsStatus == 2" @click="showNews()">Hiện</div>
    <div @click="deleteNews()">Xóa</div>
  </div>
</template>
    
  <script>
export default {
  name: "MenuFeature",
  data() {
    return {
      // news đã hiện menu
      news: null,
      // Là menu đang mở
      isOpen: false,
      // Vị trí hiển thị của menu
      left: 0,
      top: 0,
    };
  },
  methods: {
    async acceptNews() {
      if (!this.news.NewsId) return;

      this.news.NewsStatus = 1;

      const response = await this.$msAxios(
        "put",
        this.$msApi.NewsApi.Put(this.news.NewsId),
        this.news
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
      this.isOpen = false;
    },

    async hideNews() {
      if (!this.news.NewsId) return;

      this.news.NewsStatus = 2;

      const response = await this.$msAxios(
        "put",
        this.$msApi.NewsApi.Put(this.news.NewsId),
        this.news
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
      this.isOpen = false;
    },
    async showNews() {
      if (!this.news.NewsId) return;

      this.news.NewsStatus = 1;

      const response = await this.$msAxios(
        "put",
        this.$msApi.NewsApi.Put(this.news.NewsId),
        this.news
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
      this.isOpen = false;
    },

    async deleteNews() {
      if (!this.news.NewsId) return;

      this.news.NewsStatus = 1;

      const response = await this.$msAxios(
        "delete",
        this.$msApi.NewsApi.Put(this.news.NewsId)
      );

      if (response?.data) {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Success,
          message: "Thành công",
        });
      } else {
        this.$msEmitter.emit("addNotice", {
          type: this.$msEnum.NoticeType.Error,
          message: "Thất bại",
        });
      }
      this.isOpen = false;
    },
  },
  created() {
    // Toggle menu
    this.$msEmitter.on("toggleMenuFeature", (event, news) => {
      // Chủ của menu được kích hoạt
      this.news = news;
      this.isOpen = !this.isOpen;
      if (!this.isOpen || !this.$refs.tiepMenu) return;

      let menuWidth = 100;
      let menuHeight = 100;

      // Tính toán tọa độ mở ra menu
      let right = event.clientX + menuWidth;
      let left = event.clientX - menuWidth;

      if (right > window.innerWidth) {
        this.left = left + 50;
      } else if (left < 0) {
        this.left = event.clientX;
      } else {
        this.left = event.clientX - menuWidth / 2;
      }

      if (event.clientY + menuHeight > window.innerHeight)
        this.top = event.clientY - menuHeight;
      else this.top = event.clientY + 20;
    });
  },
  beforeUnmount() {
    // Hủy sự kiện
    this.$msEmitter.off("toggleMenuFeature");
  },
};
</script>
    <style lang="scss">
.menu__list {
  display: block;
  position: fixed;
  top: 0px;
  left: 0px;
  background-color: white;
  border: 1px var(--border-input-color) solid;
  border-radius: 2px;
  z-index: 2;
  min-width: 80px;
  cursor: pointer;

  > div {
    margin: 2px 1px;
    padding: 5px;
    border-radius: 2px;

    &:hover {
      color: var(--color-light-green-500);
      background-color: var(--border-input-color);
    }
  }
}
</style>