from django.db import models

# Create your models here.
class Actor(models.Model):
    actor_id = models.IntegerField(primary_key=True)
    actor_FIO = models.CharField(max_length = 100, help_text="ФИО актера")
    actor_data_birthday = models.DateField(auto_now = False)
    actors_country = models.CharField(max_length=50)
    actor_photo = models.ImageField()
    
    def __str__(self):
        return self.actor_FIO
    
    def get_absolute_url(self):
    # Returns the url to access a particular instance of the model.
        return reverse('model-detail-view', args=[str(self.id)])
    
class Genre(models.Model):
    genre_id = models.IntegerField(primary_key=True)
    genre_name = models.CharField(max_length=30)
    
    def __str__(self):
        return self.genre_name
    
    def get_absolute_url(self):
   # Returns the url to access a particular instance of the model. """
        return reverse('model-detail-view', args=[str(self.id)])
    
class Producer(models.Model):
    producer_id = models.IntegerField(primary_key=True)
    producer_FIO = models.CharField(max_length = 100, help_text="ФИО режиссера")
    producer_data_birthday = models.DateField(auto_now = False)
    producer_photo = models.ImageField()
    
    def __str__(self):
        return self.producer_FIO
    
    def get_absolute_url(self):
    # Returns the url to access a particular instance of the model. """
        return reverse('model-detail-view', args=[str(self.id)])
    
class Film_company(models.Model):
    company_id = models.IntegerField(primary_key=True)
    name_company = models.CharField(max_length = 100, help_text="Название компании")
    year_of_foundation = models.IntegerField()
    
    def __str__(self):
        return self.name_company
    
    def get_absolute_url(self):
    # Returns the url to access a particular instance of the model. """
        return reverse('model-detail-view', args=[str(self.id)])
    
class Film(models.Model):
    film_id = models.IntegerField(primary_key=True)
    name_film = models.CharField(max_length = 100, help_text="Название фильма")
    description = models.TextField()
    year_of_release = models.IntegerField()
    film_contry = models.CharField(max_length=50)
    long = models.TimeField(auto_now = False)
    rating = models.IntegerField()
    poster = models.ImageField() # или FileField?
    trailer = models.FileField()
    video = models.FileField()
    
    actor_id = models.ManyToManyField(Actor)
    genre_id = models.ManyToManyField(Genre)
    producer_id = models.ForeignKey(Producer, on_delete = models.SET_NULL, null=True)
    company_id = models.ForeignKey(Film_company, on_delete = models.SET_NULL, null=True)
    
    def __str__(self):
        return self.name_film
    
    def get_absolute_url(self):
    # Returns the url to access a particular instance of the model. """
        return reverse('model-detail-view', args=[str(self.id)])

