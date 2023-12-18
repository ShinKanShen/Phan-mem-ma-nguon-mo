<template>
  <div class="posts" @scroll="onScroll">
    <div class="bl"></div>
    <TPost v-for="news in newsList" :key="news" :news="news"></TPost>
  </div>
</template>

<script>
import TPost from "./TPost.vue";
export default {
  name: "TPostList",
  components: { TPost },
  props: ["filter"],
  async created() {
    await this.loadData();
  },
  data() {
    return {
      newsList: [],
      pageNumber: 1,
    };
  },
  methods: {
    async onScroll({ target: { scrollTop, clientHeight, scrollHeight } }) {
      if (scrollTop + clientHeight + 2 >= scrollHeight) {
        this.pageNumber++;

        this.loadData();
      }
    },
    async loadData() {
      let filter = {};
      try {
        filter = JSON.parse(this.filter);
      } catch {
        //
      }
      const response = await this.$msAxios("get", this.$msApi.NewsApi.Paging, {
        params: {
          // Kích thước của trang
          PageSize: 20,
          // vị trí trang
          PageNumber: this.pageNumber,
          SearchTerm: filter.SearchTerm,
          Category: filter.Category,
          NewsType: filter.NewsType,
          NewsStatus: 1,
          ProvinceId: filter.ProvinceId,
        },
      });
      if (response?.data?.Data) {
        for (const key in response.data.Data) {
          const element = response.data.Data[key];
          this.newsList.push(element);
        }
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.posts {
  padding-top: 12px;
  display: flex;
  gap: 12px;
  flex-wrap: wrap;
  height: calc(100vh - 50px);
  overflow-x: auto;

  justify-content: center;
  .bl {
    width: fit-content;
  }

  &::-webkit-scrollbar {
    width: 5px;
    height: 5px;
  }

  /* Handle */
  &::-webkit-scrollbar-thumb {
    background: #ccc;
  }

  /* Handle on hover */
  &::-webkit-scrollbar-thumb:hover {
    background: #a7a7a7;
  }
}
</style>