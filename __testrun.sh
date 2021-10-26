APP_NAME="emailsender-webapi"
docker build . -t "$APP_NAME"
docker run --rm -it -p 5008:80 "$APP_NAME"