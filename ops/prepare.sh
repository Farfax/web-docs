#!/bin/bash

openssl aes-256-cbc -K $encrypted_0f6049a23811_key -iv $encrypted_0f6049a23811_iv -in secrets.tar.enc -out secrets.tar -d
#openssl aes-256-cbc -K $encrypted_5704401eb47b_key -iv $encrypted_5704401eb47b_iv -in secrets.tar.enc -out secrets.tar -d
tar xvf secrets.tar
mv cert.pfx WebDocs/cert.pfx
eval "$(ssh-agent -s)"
chmod 600 ./$KEY_FILE
echo -e "Host $SERVER_IP\n\tStrictHostKeyChecking no\n" >> ~/.ssh/config
ssh-add ./$KEY_FILE
