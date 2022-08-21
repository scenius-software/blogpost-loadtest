import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '30s', target: 100 }, // Ramp up
        { duration: '2m', target: 100 }, // Hold
        { duration: '30s', target: 0 }, // Ramp down
    ],
    thresholds: {
        'http_req_duration': ['p(95)<150'], // 99% of requests must complete below 1.5s
    }
};

export default () => {
    http.get('http://localhost:5000/data');
    sleep(1);
};