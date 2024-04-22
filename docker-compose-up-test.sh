#!/bin/bash
# PREPARE CONFIGURATION FILES
cp docker-compose-test.yml docker-compose-run.yml
cp EmailSender.ClientApp/nginx/nginx-http.conf EmailSender.ClientApp/nginx/nginx.conf

# SET ENVIRONMENT VARIABLES
APP_BACKEND="localhost:6000"
SERVER_NAME="localhost"

# APPLY ENVIRONMENT VARIABLES
sed -i "" \
-e "s/\${SERVER_NAME}/${SERVER_NAME}/" \
-e "s/\${APP_BACKEND}/${APP_BACKEND}/" \
EmailSender.ClientApp/nginx/nginx.conf 

# RUN DOCKER COMPOSE
docker compose -f docker-compose-run.yml up -d
