# NewsKaspi

Для запуска приложения необходимо пройти в директорию Api и запустить NewsProject.sln, далее внутри vs в качестве запускаемого проекта указать NewsProject

Через Postman вызываем POST метод https://localhost:7119/api/login и получаем JWT token
В Postman раздел Authorization, тип type выбираем "Bearer Token" и там появится поле куда и вставляем JWT token
Далее в методах видно по атрибуту Roles по какой роли можно обратиться к нему

Примеры даты: 2022.06.21

Логины и пароли хранятся в классе UsersCreater

Из библиотек использовались:
AgilityPack, Fizzler для парсинга, удобно обращаться по селекторам в HTML
EntityFrameworkCore для работы с базой данных
JwtBearer для авторизации по JWT токену
SwashBuckle для swagger
NLog для логирования

Старался придерживаться принципа Dependency Inversion поэтому реализовал через внедрение зависимостей и интерфейсы а также разграничил на уровни
