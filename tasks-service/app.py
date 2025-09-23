from flask import Flask, request, jsonify, abort
from pydantic import BaseModel, ValidationError, constr
from typing import List
import sqlite3
import os

app = Flask(__name__)

DB_PATH = os.environ.get('DATABASE_URL', 'sqlite:///data/tasks.db').replace('sqlite:///', '')

def init_db():
    os.makedirs(os.path.dirname(DB_PATH) or '.', exist_ok=True)
    conn = sqlite3.connect(DB_PATH)
    conn.execute('CREATE TABLE IF NOT EXISTS tasks (id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT NOT NULL, description TEXT)')
    conn.commit()
    conn.close()

class TaskIn(BaseModel):
    title: constr(min_length=1, max_length=120)
    description: constr(max_length=1000) = ""

# Inicialización explícita (no usar decorator para compatibilidad)
try:
    init_db()
except Exception as e:
    # Log básico; en producción usa logging
    print("Warning: could not initialize DB at startup:", e)

@app.route("/")
def index():
    return jsonify({"status": "tasks-service running"})

@app.route("/tasks", methods=["GET"])
def list_tasks():
    conn = sqlite3.connect(DB_PATH)
    cursor = conn.execute('SELECT id, title, description FROM tasks')
    tasks = [{"id": r[0], "title": r[1], "description": r[2]} for r in cursor.fetchall()]
    conn.close()
    return jsonify(tasks)

@app.route("/tasks", methods=["POST"])
def create_task():
    try:
        payload = request.get_json() or {}
        task = TaskIn(**payload)
    except ValidationError as e:
        return jsonify({"error": e.errors()}), 400

    conn = sqlite3.connect(DB_PATH)
    cur = conn.execute('INSERT INTO tasks (title, description) VALUES (?, ?)', (task.title, task.description))
    conn.commit()
    task_id = cur.lastrowid
    conn.close()
    return jsonify({"id": task_id, "title": task.title, "description": task.description}), 201

if __name__ == "__main__":
    # Si quieres modo debug local, usa FLASK_ENV=development o debug=True
    app.run(host="0.0.0.0", port=5000)
