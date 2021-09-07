# Примеры работы с docker \ docker-compose

## Sample0 - Простой пример Hello World

`docker run hello-world`

## Sample1 - Собираем образ для запуска приложения на python

`docker build ./ -t my-python-app`

`docker image ls`
`docker images`

`docker run my-python-app`

`docker ps`
`docker ps -a`

`docker run --name my-app my-python-app`

`docker ps -a -q`
`docker rm $(docker ps -qa)`

`docker run --name my-app --rm my-python-app`

## Sample2 - Добавляем в предыдущий пример переменную окружения 

`docker build ./ -t my-python-app`

`docker run --name my-app --rm my-python-app
`docker run --name my-app -e NAME=World --rm my-python-app`


## Sample2 - Создаем Dockerfile для веб-приложения, прокидываем порт

`docker build ./ -t webapp`

`docker run --name my-web-app --rm -d -p 5000:80 webapp`

## Sample3 - Создаем docker-compose.yml для запуска нескольких сервисов: наше веб-приложения + postgres + pgadmin

- Для запуска контейнеров выполняем:

	`docker-compose pull`
	`docker-compose up -d`
	
- Создает **persisted volumes** персистентный сервис для работы с БД (PostgreSQL), который биндится на дефолтные порты:
	PostgreSQL
		- 5432:5432
  
- Отключение: 
  
  `docker-compose down` 
  
  (порты свободны, но **persisted volumes** остаются).