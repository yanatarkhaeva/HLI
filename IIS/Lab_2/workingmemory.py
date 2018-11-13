class WorkingMemory:
    def __init__(self):
        self.fact_dict = {}
        pass

    def add_fact(self, name, value=None):
        self.fact_dict[name] = value

    def get_facts(self):  #
        return self.fact_dict


class IWorkingMemory(WorkingMemory):
    def __init__(self):
        super().__init__()

    def add_fact(self, name, value=None):
        """Добавление фактов в рабочую память"""
        super().add_fact(name, value=None)
        pass

    def get_facts(self):
        """Получение словаря фактов"""
        return super().get_facts()
        pass