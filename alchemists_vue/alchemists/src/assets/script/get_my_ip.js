import axios from 'axios';

export async function get_my_ip() {
    try {
        const my_ip = await axios.get('https://api.ipify.org?format=json');
        console.log(my_ip.data);
        return my_ip.data.ip;
        
    } catch (error) {
        console.error(error);
        return 'fail';
    }
}