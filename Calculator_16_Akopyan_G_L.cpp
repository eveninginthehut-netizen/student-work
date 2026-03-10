#include <iostream>//библиотека ввода и вывода 
#include <string>//для работы со строками
#include <sstream>//для преобразования строк в числа  
#include <iomanip>//для форматированя вывода 

//функция для ввода 16 ричного числа, принимает строку подсказку, возвращает число в 10-ом виде 
int a = 0; 
int b = 0;
int result = 0;

int inputHex(std::string promt) {
    std::string input;
    int number;
    std::cout << promt;
    getline(std::cin, input);
    std::stringstream ss; //создаем поток для преобразования строки в число
    ss <<std::hex << input;//записываем строку в поток, hex означает что число в  16 ричной системе 
    ss >> number;//преобразуем поток в число
    return number;
}
//функция сложения на асемблере, принимает 2 числа и возврашает их сумму
int addAsm(int x, int y) {
    int result_asm = 0;
    __asm {
        mov eax,x //загружаем 1-ое число
        mov ebx,y //загружаем 2-ое число
        add eax,ebx // складываем в eax
        mov result_asm,eax // сохраняем результат из eax в переменную

    }
    return result_asm;
}

int main()
{
    setlocale(LC_ALL, "Rus");
    std::cout << "=====================" << std::endl;
    std::cout << "введите числа в HEX (например: 1F,A3,100)"<<std::endl;
    a = inputHex("Введите 1-ое число в HEX: ");
    b = inputHex("Введите 2-ое число в HEX: ");
    result = addAsm(a, b);

    std::cout << "Результат: " << result;
    return 0;
}

