#stage 1
FROM node:latest as node
WORKDIR /app
EXPOSE 4200
COPY . .
RUN npm install
RUN npm run build --prod
#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/AngularFront /usr/share/nginx/html
CMD ["npm", "run", "start"]
