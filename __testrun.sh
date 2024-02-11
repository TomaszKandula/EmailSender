ENV_VALUE="Development"
APP_NAME="emailsender-test"

docker build . --build-arg "ENV_VALUE=$ENV_VALUE" -t "$APP_NAME"
docker run --rm -it -p 7008:80 "$APP_NAME"
