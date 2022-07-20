FROM node:18-alpine3.15 AS builder
ARG ApiLocation
ENV NODE_ENV production
ENV REACT_APP_API_ENDPOINT ${ApiLocation}

RUN echo $REACT_APP_API_ENDPOINT
RUN echo $PATH
# Add a work directory
WORKDIR /app
# Cache and Install dependencies
COPY package.json .
#COPY yarn.lock .
RUN npm install --production
# Copy app files
COPY . .
# Build the app
RUN npm run build

# Bundle static assets with nginx
FROM nginx:1.21.0-alpine as production
ENV NODE_ENV production
# Copy built assets from builder
COPY --from=builder /app/build /usr/share/nginx/html
# Add your nginx.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf
# Expose port
EXPOSE 80
# Start nginx
CMD ["nginx", "-g", "daemon off;"]