from vkbottle.bot import Bot, Message
TOKEN="vk1.a.tXop3P15u6v5gxGJge-MB2qSbg-9tOBpS4IsFzGAUWLquD44LfmuuTeJVcP8z3i4Z4vIgfaZCklr60TOEm_4aTNKtVcszbQZEXsdqIPVK00s806MAWAvFh9sYqMVlzfGQtGZSG-sfBiI-nAYGc-2g5ZNzZhd12tY-JyOL_F6Qr8ak3wwU3r1sAUfcuBoIoiifHz69gc1rAU7KjTB_ji7dg"

bot=Bot(TOKEN)
#команда /start
@bot.on.message(text="/start")
async def start_handler(message:Message):
    await message.answer("Привет я твой первый бот в вконтакте!")
#Ответ на любое сообщение "Привет"
@bot.on.messsage(text="Привет")
async def hi_handler(message:Message):
    user_id=message.from_id
    await message.answer(f"И тебе привет id пользователя {user_id}") 
#обработка остального текста
@bot.on.message()
async def my_message(message:Message):
    text=message.text
    if text:
        await message.answer(f"Ты написал {text}, молодец {user_id} и Гор!")
if name=="main":
    print("Бот запущен!")
    bot.run_forever()