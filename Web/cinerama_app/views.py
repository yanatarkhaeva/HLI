from django.shortcuts import render
from django.http import HttpResponse
from django.template import loader

from .models import Film
from .models import Actor

# Create your views here.
# def index(request):
    # return HttpResponse("Hello, world. You're at the Cinerama index.")

def index(request):
    # Генерация "количеств" некоторых главных объектов
    num_films = Film.objects.all().count()
    num_actors = Actor.objects.count()
    template = loader.get_template( 'index.html')
    context = {
        'num_films':num_films,       'num_actors':num_actors,
    }
    return HttpResponse(template.render(context, request))
    
def welcome(request):
    # Генерация "количеств" некоторых главных объектов
    template = loader.get_template( 'welcome.html')
    context = { }    
    # Отрисовка HTML-шаблона index.html с данными внутри переменной контекста context
    return HttpResponse(template.render(context, request))