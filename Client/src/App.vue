<script setup>
import { onMounted } from "vue";

let response, resultData;

onMounted(() => {
  console.log(`the component is now mounted.`);

  response = fetch("http://localhost:5049/api/Products/GetProducts")
    .then(function (response) {
      return response.json();
    })
    .then(function (data) {
      console.log("data: ", data);
      resultData = data;
    });
});

function getImagePath(imageName) {
  //return "./assets/images/char.jpeg";
  return "./assets/images/" + imageName;
}
</script>

<template>
  12245

  <div id="v-for-object" class="cards">
    <div v-for="item in resultData" class="card">
      <img class="card__image" :src="getImagePath(item.image)" />

      <div class="card__title" id="name">{{ item.name }}</div>
      <div>
        <div class="card__price" id="price">{{ item.price }}</div>
        ש"ח
      </div>
    </div>
  </div>
</template>

<style scoped>
.cards {
  display: grid;
  grid-template-columns: repeat(3, 262px);
  gap: 10px;
}

.card {
  display: flex;
  flex-direction: column;
  align-items: center;

  border-radius: 6px 6px 0 0;

  background-color: white;
  border: 1px solid white;
  position: relative;

  transition: border 0.5s linear 0s;
  padding: 10px 0 0 0;

  width: 260px;
  border: 1px solid #b1b1b1;
}

.card:hover {
  border: 1px solid #717171;
}

.card__title {
  border: 0px;
  font-size: 26px;
}

.card__title:disabled {
  background-color: white;
}

.card__title:focus {
  outline: none;
}

.card__price {
  border: 0px;
  font-size: 26px;
  width: 180px;
  text-align: left;
  margin-top: 20px;
}

.card__price:disabled {
  background-color: white;
}

.card__price:focus {
  outline: none;
}

.card__image {
  max-width: 200px;
  height: 200px;
  margin: 0 auto;
  border-radius: 10px;
}

.card__image:hover {
  /*transform: scale(1.05);*/
}

.card .card__image {
  transition: transform 0.5s linear 0s;
}

.card__button {
  background-color: transparent;
  border: 0;
  cursor: pointer;
  padding: 0;
  margin: 0;
  visibility: visible;
}

.card__button_hidden {
  visibility: hidden;
}

.card:hover .card__image {
  transform: scale(1.02);
}

.card__icon {
  width: 50px;
  transition: transform 0.3s linear 0s;
  color: rgb(70, 70, 70);
  opacity: 0.8;
}

.card__icon:hover {
  transform: scale(1.1);
}

.card__counter {
  display: flex;
  flex-direction: row;
  align-items: center;
  position: absolute;
  top: 100px;
}

.card__counter_hidden {
  display: none;
}

.card__count {
  font-size: 50px;
  color: rgb(70, 70, 70);
  opacity: 0.8;
  padding: 0 10px;
}

.card__title {
  width: 200px;
  margin: 0;
  padding: 0;
}
</style>
