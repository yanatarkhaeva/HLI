from django.contrib import admin
# Register your models here.
from .models import Actor, Genre, Producer, Film_company, Film

# Register your models here.
admin.site.register(Actor)
admin.site.register(Producer)
admin.site.register(Genre)
admin.site.register(Film_company)
admin.site.register(Film)