# Инструкция по развертыванию локальной инфраструктуры

## Развертывание БД в *docker*-контейнере

- Для запуска контейнера с БД выполнить в корневом каталоге:

	`docker-compose pull`

	`docker-compose up -d`
- Создает **persisted volumes** персистентный сервис для работы с БД (PostgreSQL \ MS SQL Server), который биндится на дефолтные порты:
	PostgreSQL
		- 5432:5432
  
	MSSQL 
		- 1433:1433
  
- Отключение: 
  
  `docker-compose down` 
  
  (порты свободны, но **persisted volumes** остаются).