FROM node:18-alpine AS build


WORKDIR /app
COPY package.json .
RUN npm install --only=prod
COPY . .

ARG server_url
ARG file_server_url
ARG file_metadata_server_url

ENV REACT_APP_SERVER_URL ${server_url}
ENV REACT_APP_FILE_SERVER_URL ${file_server_url}
ENV REACT_APP_FILE_METADATA_SERVER_URL ${file_metadata_server_url}

RUN npm run build

FROM nginx:1.23-alpine
COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
