FROM node:alpine

RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app

COPY package.json .

RUN npm install --verbose

EXPOSE 8080

CMD ["npm", "run", "build"]
