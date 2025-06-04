# VisualNovel
SOLID-принципы и паттерны:

1) Single Responsibility:
- DialogueSystem - только логика диалога
- DialogueUIController - только отображение
- CharacterDatabase - управление спрайтами
2) Open/Closed:
- Расширение через ScriptableObjects
- Добавление новых типов узлов через наследование
3) Dependency Inversion:
- Событийная система через UnityActions
- Сервис-локатор для баз данных

Используемые паттерны:
1) Observer (события диалога)
2) Singleton (менеджеры)
3) Service Locator (базы данных)
4) State Machine (узлы диалога)
