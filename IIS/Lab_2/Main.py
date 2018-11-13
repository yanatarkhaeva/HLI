from workingmemory import IWorkingMemory
from knowledgebase import IKnowledgeBase
from inferencemachine import InferenceMachine

import tkinter as tk
from tkinter import messagebox


class MainWindow():
    def __init__(self):
        super().__init__()
        self.working_memory = IWorkingMemory()
        self.knowledge_base = IKnowledgeBase(self.working_memory)
        self.inference_machine = InferenceMachine(self.working_memory, self.knowledge_base)

        self.initUI()

    def initUI(self):
        root.minsize(width=600, height=300)
        root.title('Экспертная система "На костылях"')

        self.answer_frame = tk.Frame(root)
        self.answer_frame.pack()

        self.start_label = tk.Label(self.answer_frame, text="Начать диагностику?")
        self.start_label.pack()
        self.start_button = tk.Button(self.answer_frame, text="Начать", command=lambda: self.ask_question_get_answer())
        self.start_button.pack()

        menu = tk.Menu(root)
        root.config(menu=menu)
        exe_menu = tk.Menu(menu)

        exe_menu.add_command(label="Repeat", command=lambda: self.restart())
        menu.add_cascade(label="Repeat", menu=exe_menu)

    def restart(self):
        for key in self.answer_frame.children:
            self.answer_frame.children[key].pack_forget()
        self.start_label = tk.Label(self.answer_frame, text="Начать диагностику?")
        self.start_label.pack()
        self.start_button = tk.Button(self.answer_frame, text="Начать", command=lambda: self.ask_question_get_answer())
        self.start_button.pack()

    def ask_question_get_answer(self):
        self.start_label.pack_forget()
        self.start_button.pack_forget()
        question = self.inference_machine.start(self)
        self.question_label = tk.Label(self.answer_frame, text=question)
        self.question_label.pack()

        if "Так как" not in str(question):
            self.answer_entry = tk.Entry(self.answer_frame, width=40)
            self.answer_entry.pack()
            self.answer_button = tk.Button(self.answer_frame, text="Ответить", command=lambda: self.get_answer())
            self.answer_button.pack()
            if "Мы" in str(question):
                self.answer_entry.pack_forget()
                self.answer_button.pack_forget()
        else:
            if "диагноз" in str(question):
                self.answer_entry.pack_forget()
                self.answer_button.pack_forget()
            else:
                self.get_rule_worked()
        pass

    def get_rule_worked(self):
        self.inference_machine.set_answer()
        self.ask_question_get_answer()

    def get_answer(self):
        answer = self.answer_entry.get()
        if str(answer) == "да" or str(answer) == "нет" or "частота сердечного ритма" in str(self.question_label.cget('text')):
            self.inference_machine.set_answer(answer)
            self.answer_entry.pack_forget()
            self.answer_button.pack_forget()
            label = tk.Label(self.answer_frame, text=answer)
            label.pack()
            self.ask_question_get_answer()
        else:
            messagebox.showinfo("Ошибка", "Введите корректный ответ")
            self.answer_entry.delete(0, 'end')

if __name__ == '__main__':
    root = tk.Tk()
    main_window = MainWindow()
    root.mainloop()