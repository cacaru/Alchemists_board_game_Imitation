<template>
    <div class="offcanvas offcanvas-start" id="my_item" data-bs-backdrop="false" tabindex="-1" aria-labelledby="board_my_item" data-bs-scroll="true">
        <div class="offcanvas-header">
            <p class="offcanvas-title" id="board_my_item_title">내 아이템</p>
            <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body" v-for="data in this.user_data" :key="data.user_key">
            <b-container id="item_container" v-if="this.my_key === data.user_key">
                <b-row>
                    <b-col>
                        재료카드
                        <b-row class="my_item_card">
                            <b-col>
                                <img src="@/assets/img/material_card/card_1.png">
                                {{ data.user_ingame_data.ingredient.card_1 }} 개
                            </b-col>
                            <b-col>
                                <img src="@/assets/img/material_card/card_2.png">
                                {{ data.user_ingame_data.ingredient.card_2 }} 개
                            </b-col>
                        </b-row>
                        <b-row class="my_item_card">
                            <b-col>
                                <img src="@/assets/img/material_card/card_3.png">
                                {{ data.user_ingame_data.ingredient.card_3 }} 개
                            </b-col>
                            <b-col>
                                <img src="@/assets/img/material_card/card_4.png">
                                {{ data.user_ingame_data.ingredient.card_4 }} 개
                            </b-col>
                        </b-row>
                        <b-row class="my_item_card">
                            <b-col>
                                <img src="@/assets/img/material_card/card_5.png">
                                {{ data.user_ingame_data.ingredient.card_5 }} 개
                            </b-col>
                            <b-col>
                                <img src="@/assets/img/material_card/card_6.png">
                                {{ data.user_ingame_data.ingredient.card_6 }} 개
                            </b-col>
                        </b-row>
                        <b-row class="my_item_card">
                            <b-col>
                                <img src="@/assets/img/material_card/card_7.png">
                                {{ data.user_ingame_data.ingredient.card_7 }} 개
                            </b-col>
                            <b-col>
                                <img src="@/assets/img/material_card/card_8.png">
                                {{ data.user_ingame_data.ingredient.card_8 }} 개
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        호의 카드
                        <b-row>
                            <b-col class="favor_area">
                                <b-row>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/assistent.png" v-on:click="open_favor_modal('assistent')">
                                        {{ data.user_ingame_data.favor_card.assistent }} 개
                                    </b-col>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/bar_owner.png" v-on:click="open_favor_modal('bar_owner')">
                                        {{ data.user_ingame_data.favor_card.bar_owner }} 개
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/big_man.png" v-on:click="open_favor_modal('big_man')">
                                        {{ data.user_ingame_data.favor_card.big_man }} 개
                                    </b-col>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/caretaker.png" v-on:click="open_favor_modal('caretaker')">
                                        {{ data.user_ingame_data.favor_card.caretaker }} 개
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/herbalist.png" v-on:click="open_favor_modal('herbalist')">
                                        {{ data.user_ingame_data.favor_card.herbalist }} 개
                                    </b-col>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/merchant.png" v-on:click="open_favor_modal('merchant')">
                                        {{ data.user_ingame_data.favor_card.merchant }} 개
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/shopkeeper.png" v-on:click="open_favor_modal('shopkeeper')">
                                        {{ data.user_ingame_data.favor_card.shopkeeper }} 개
                                    </b-col>
                                    <b-col>
                                        <img src="@/assets/img/favor_card/wise_man.png" v-on:click="open_favor_modal('wise_man')">
                                        {{ data.user_ingame_data.favor_card.wise_man }} 개
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        협상 카드
                        <b-row>
                            <b-col v-if="data.user_ingame_data.discount_adventurer.ad_0 === true ">
                                <img class="adventurer_nego_token" src="@/assets/img/adventurer/ad_0.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.discount_adventurer.ad_1 === true ">
                                <img class="adventurer_nego_token" src="@/assets/img/adventurer/ad_1.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.discount_adventurer.ad_2 === true ">
                                <img class="adventurer_nego_token" src="@/assets/img/adventurer/ad_2.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.discount_adventurer.ad_3 === true ">
                                <img class="adventurer_nego_token" src="@/assets/img/adventurer/ad_3.png">
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        인장
                        <b-row>
                            <b-col v-if="data.user_ingame_data.have_stamp.point_5_1 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_5.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_5.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_5.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_5.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.point_5_2 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_5.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_5.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_5.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_5.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.point_3_1 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_3.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_3.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_3.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_3.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.point_3_2 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_3.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_3.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_3.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_3.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.point_3_3 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_3.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_3.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_3.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_3.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_red_1 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_red.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_red.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_red.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_red.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_red_2 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_red.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_red.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_red.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_red.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_blue_1 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_blue.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_blue.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_blue.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_blue.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_blue_2 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_blue.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_blue.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_blue.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_blue.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_green_1 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_green.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_green.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_green.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_green.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.have_stamp.question_green_2 == true ">
                                <img v-if="data.user_color == 'black' " src="@/assets/img/stamp/stamp_black_green.png">
                                <img v-else-if="data.user_color == 'red' " src="@/assets/img/stamp/stamp_red_green.png">
                                <img v-else-if="data.user_color == 'blue' " src="@/assets/img/stamp/stamp_blue_green.png">
                                <img v-else-if="data.user_color == 'white' " src="@/assets/img/stamp/stamp_white_green.png">
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col class="artifacts_area">
                        구매한 유물
                        <b-row>
                            <b-col v-if="data.user_ingame_data.artifacts.bronze_cup === true" v-b-modal.bronze_cup>
                                <img src="@/assets/img/artifacts/rank_3/bronze_cup.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.chest_of_witch === true" v-b-modal.chest_of_witch>
                                <img src="@/assets/img/artifacts/rank_2/chest_of_witch.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.discount_card === true" v-b-modal.discount_card>
                                <img src="@/assets/img/artifacts/rank_1/discount_card.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.eloquent_necklace === true" v-b-modal.eloquent_necklace>
                                <img src="@/assets/img/artifacts/rank_2/eloquent_necklace.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.feather_hat === true" v-b-modal.feather_hat>
                                <img src="@/assets/img/artifacts/rank_3/feather_hat.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.glass_cabinet === true" v-b-modal.glass_cabinet>
                                <img src="@/assets/img/artifacts/rank_3/glass_cabinet.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.golden_alter === true" v-b-modal.golden_alter>
                                <img src="@/assets/img/artifacts/rank_3/golden_alter.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.haste_boots === true" v-b-modal.haste_boots>
                                <img src="@/assets/img/artifacts/rank_1/haste_boots.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.hypnotic_necklace === true" v-b-modal.hypnotic_necklace>
                                <img src="@/assets/img/artifacts/rank_2/hypnotic_necklace.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.magic_mirror === true" v-b-modal.magic_mirror>
                                <img src="@/assets/img/artifacts/rank_3/magic_mirror.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.magic_mortar === true" v-b-modal.magic_mortar>
                                <img src="@/assets/img/artifacts/rank_1/magic_mortar.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.night_vision === true" v-b-modal.night_vision>
                                <img src="@/assets/img/artifacts/rank_1/night_vision.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.printing_machine === true" v-b-modal.printing_machine>
                                <img src="@/assets/img/artifacts/rank_1/printing_machine.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.robe_of_respect === true" v-b-modal.robe_of_respect>
                                <img src="@/assets/img/artifacts/rank_1/robe_of_respect.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.seal_of_authority === true" v-b-modal.seal_of_authority>
                                <img src="@/assets/img/artifacts/rank_2/seal_of_authority.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.silver_glass === true" v-b-modal.silver_glass>
                                <img src="@/assets/img/artifacts/rank_2/silver_glass.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.statue_of_wisdom === true" v-b-modal.statue_of_wisdom>
                                <img src="@/assets/img/artifacts/rank_3/statue_of_wisdom.png">
                            </b-col>
                            <b-col v-if="data.user_ingame_data.artifacts.thinking_hat === true" v-b-modal.thinking_hat>
                                <img src="@/assets/img/artifacts/rank_2/thinking_hat.png">
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-container>
        </div>
    </div>
    
</template>

<script>

export default {
    name : "personal_game_data",
    props: {
        user_data : {
            type : Array,
            required : true
        },
        my_key : {
            type : String,
            required : true,
        }
    },
    methods : {
        open_favor_modal : function(card_kind) {
            this.$emit('favor_card_modal_on', card_kind);
        },
    }
}

</script>

<style src="@/assets/css/personal_game_data.css">
</style>