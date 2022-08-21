clear
docker run -v ${pwd}/k6-load-test.js:/home/k6/script.js  --rm -it   grafana/k6 run /home/k6/script.js
pause