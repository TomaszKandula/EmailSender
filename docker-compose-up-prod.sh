#!/bin/bash
# PREPARE DOCKER COMPOSE FILE
cp docker-compose-prod.yml docker-compose-run.yml
cp EmailSender.ClientApp/nginx/nginx-https.conf EmailSender.ClientApp/nginx/nginx.conf

# SET ENVIRONMENT VARIABLES
SERVER_NAME="emailsender.dev"

# APPLY ENVIRONMENT VARIABLES
sed -i \
-e "s/\${SERVER_NAME}/${SERVER_NAME}/" \
EmailSender.ClientApp/nginx/nginx.conf 

# RUN DOCKER COMPOSE
docker compose -f docker-compose-run.yml up -d
