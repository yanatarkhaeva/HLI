class InferenceMachine:
    def __init__(self, working_memory, knowledge_base):  # use antipod interfaces here
        self.working_memory = working_memory
        self.knowledge_base = knowledge_base
        self.item_on_work = None
        self.answer = ""
        self.count_quest = 0
        for i in knowledge_base.rules:
            if str(type(i))[22:-2] == "KnowledgeBase.Question":
                self.count_quest += 1
        pass

    def start(self, main):
        return self.check_facts(main)

    # TODO: куда add -- лучше в базу знаний, а то получается уроборос
    def add_question(self, name, preconditions, question):
        question = self.knowledge_base.Question(name, preconditions, question)
        pass

    def add_rule(self, name, preconditions, insert):
        rule = self.knowledge_base.Rule(name, preconditions, insert)
        pass

    def check_facts(self, main):
        facts = self.working_memory.get_facts()
        rules = self.knowledge_base.get_rules()
        if facts['диагноз'] is None and self.count_quest > 0:
            return self.interview(main, facts, rules)

    def interview(self, main, facts, rules):
        for i in rules:
            if i.is_used is False:
                is_coincided = True
                for j in i.preconditions.keys():
                    if i.preconditions[j] != str(facts[j]):
                        is_coincided = False
                        break
                if is_coincided is True:
                    self.item_on_work = i
                    self.item_on_work.is_used = True
                    return self.item_on_work.question
        return ("Мы не знаем что с вами")

        pass

    def set_answer(self, answer=None):
        facts = self.working_memory.get_facts()
        if answer is None:
            self.item_on_work.update_facts(facts)
        else:
            self.answer = answer
            self.item_on_work.update_facts(facts, answer)
            self.count_quest -= 1
        self.item_on_work.is_used = True
    pass


class IInferenceMachine(InferenceMachine):
    def __init__(self):
        super().__init__()

    pass



