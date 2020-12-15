FROM node

# Update to the latest node version (default is 7.x)
RUN npm install -g n

RUN mkdir -p /frontend/app
WORKDIR /frontend/app

COPY package*.json /frontend/app/
RUN npm install

COPY . /frontend/app
RUN npm run build

CMD "npm" "run" "start"
