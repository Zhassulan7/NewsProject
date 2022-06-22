NewsProject

ƒл€ запуска приложени€ необходимо пройти в директорию Api и запустить NewsProject.sln, далее внутри vs в качестве запускаемого проекта указать NewsProject

«атем через Postman дергаем POST метод https://localhost:7119/api/login и получаем оттуда JWT token
¬ Postman раздел Authorization, тип type выбираем Bearer Token и там по€витс€ поле куда и вставл€ем JWT token
ƒалее в методах видно по атрибуту Roles по какой роли можно обратитьс€ к нему

Ћогины и пароли хран€тс€ в классе UsersCreater

»з библиотек использовались:
AgilityPack, Fizzler дл€ парсинга, удобно обращатьс€ по селекторам в HTML
EntityFrameworkCore дл€ работы с базой данных
JwtBearer дл€ авторизации по JWT токену
SwashBuckle дл€ swagger

—таралс€ придерживатьс€ принципа Dependency Inversion поэтому реализовал через внедрение зависимостей и интерфейсы а также разграничил на уровни