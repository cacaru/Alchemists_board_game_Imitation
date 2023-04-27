import join_server from '@/components/join_server.vue'

export default {
    name : 'first_page',
    components : {
        join_server : join_server,
    },
    mounted() {
       
    },
    created() {
        if( this.$route.params.refresh == "true" ){
            this.$router.go();
        }
    },
    data () {
        return {
            is_join_server : false,
            loading : false,
            name : '',
            ip : '',
        }
    },
    methods : {
        join_server: function () {
            this.is_join_server = true;
        },

        loading_start : function(data) {
            //console.log(data);
            this.name = data.name;
            this.ip = data.ip;
            this.$router.push({
                name : 'room_list',
                params : {
                    name : this.name,
                    ip : this.ip,
                    re_page : false,
                }
            });

        }
    }
}