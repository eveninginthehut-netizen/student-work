package main//главный пакет main точка входа в программу

import("encoding/json" "fmt" "log" "net/http" "string")
//Создаем модель пользователя
type User struct{
	ID int `json:"id"`
	Username string `"json:"username"`
	Email string `json:"email"`
}
//users-база данных в памяти
var users=[]User{
	{ID:1,Username:"Alise",Email:"alice@example.com"},
	{ID:2,Username:"Ferdinant",Email:"Ferdinant1@example.com"},
}
//main-tochka vhoda
func main(){
	//обработка для GET
	http.HandleFunc("/users",getUsers)
	//Обработчик для post//создает нового пользователя
	http.HandleFunc("/users/create",createUser)
	//Запускаем сервер на порту 8080
	log.Println("Сервер запущен на http//localhost:8080")
	log.Fatal(http.ListenAndServer(":8080", nil)) 
}
func getUsers(w http.ResponseWriter, r *http.Request){
	w.Header().Set("Content-Type","application/json")
	json.newEncoder(w).Encode(users)
}
func createuser(w http.ResponseWriter,r *http.Request) {
	if r.method!=http.methodPost{
		http.Error(w,"Метод не поддерживается",http.StatusMethodNotAllowed)
		return
	}
	//переменная куда будем декодировать json
	var newuser.User
	err:=json.newDecoder(r.body).Decode(&newuser)
	if err!=nil{
		http.Error(w."Неверный json",http.StatusBadRequest)
		return
	}
	//Валидация (проверим что поля не пустые)
	if strings.TrimSpace(newuser.Username)==""||strings.TrimSpace(newuser.Email)==""{
		http.Error(w,"Username и email обязательны",http.StatusBadRequsest)
		return
	}
	//генерируем новый id
	newId:=users[len(users)-1].ID+1
	newUser.ID=newId
	users-append(users,newuser)
	w.Header().Set("content-type","application/json")
	w.WriteHeader(http.StatusCreated)
	json.newEncoder(w).Encode(newuser)
}
