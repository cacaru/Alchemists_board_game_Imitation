
import { default as First_page } from '@/components/first_page.vue';
import { default as Create_lobby } from '@/components/create_lobby.vue';
import { default as Enter_lobby } from '@/components/enter_lobby.vue';
import { default as Game_lobby } from '@/components/game_lobby.vue';
import { default as Game_board } from '@/components/game_board.vue';
import { default as End_page } from '@/components/end_page.vue';
import { default as Room_list } from '@/components/room_list.vue';

export const routes = [
    {
        path: '/', 
        redirect: '/alchemists' 
    }, {
        path: '/alchemists',
        name: 'first_page',
        component: First_page
    }, {
        path: '/room_list',
        name: 'room_list',
        component: Room_list,
    }, {
        name : 'create_lobby',
        path: '/create_lobby/',
        component: Create_lobby,
    }, {
        name : 'enter_lobby',
        path: '/enter_lobby',
        component: Enter_lobby
    }, {
        name : 'game_lobby',
        path: '/game_lobby/',
        component: Game_lobby
    }, {
        name : 'game_board',
        path: '/game_board',
        component : Game_board  
    },{
        name : 'end_page',
        path: '/end_page',
        component : End_page,
    },
]
