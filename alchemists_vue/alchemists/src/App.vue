<template>
    <router-view></router-view>
</template>

<script>
</script>

<style>
body {
  /* 글씨 클릭 제거 */ 
  -webkit-user-select:none;
  -moz-user-select:none;
  -ms-user-select:none;
  user-select: none;
  /* 스크롤 숨기기 */
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}


body::-webkit-scrollbar {
    display: none; /* Chrome, Safari, Opera*/
}

</style>
